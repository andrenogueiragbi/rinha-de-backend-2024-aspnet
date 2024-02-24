import asyncio
import aiohttp
import requests


async def make_request(session, url, payload, headers, results):
    try:
        async with session.post(url, json=payload, headers=headers) as response:
            print(f"Status code for {url}: {response.status}")
            if response.status != 200:
                save_failed_response(url, response)
            results.append("Success")  # Adiciona "Success" à lista de resultados
    except asyncio.exceptions.TimeoutError:  # Captura exceção de timeout
        print(f"Timeout ao acessar {url}")
        results.append("Timeout")  # Adiciona "Timeout" à lista de resultados
    except Exception as e:
        print(f"Erro ao acessar {url}: {e}")
        results.append("Error")  # Adiciona "Error" à lista de resultados

def save_failed_response(url, response):
    print(f"Request to {url} failed with status code {response.status}")

async def main():
    url = "http://localhost:9999/clientes/1/transacoes"
    payload = {
        "valor": 1,
        "tipo": "d",
        "descricao": "toma"
    }
    headers = {"Content-Type": "application/json"}

    results = []  # Lista para armazenar os resultados de cada requisição

    async with aiohttp.ClientSession() as session:
        tasks = [make_request(session, url, payload, headers, results) for _ in range(25)]
        await asyncio.gather(*tasks)

    # Contagem de timeouts
    #timeouts = results.count("Timeout")
    #print(f"Total de timeouts: {timeouts}")
    
    
    url = "http://localhost:9999/clientes/1/extrato"

    
    response = requests.request("GET", url, headers=headers).json()
    
    
    if response["saldo"]["total"] != -25:
        print("FALHA!", response["saldo"]["total"],"DIFERENTE DE ",-25)

    else:
        print("SUCESSO NA COMCORRENCIA DÉBITO")


if __name__ == "__main__":
    asyncio.run(main())
