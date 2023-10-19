import { uri } from "@/helpers/variables"

export async function login(account) {
  const request = fetch(`${uri}/account/login`, {
    method: 'POST',
    mode: "cors",
    body: JSON.stringify(account),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })

  const [response] = await Promise.all([request])

  return response;
}

export async function register(account) {
  const request = fetch(`${uri}/account/register`, {
    method: 'POST',
    mode: "cors",
    body: JSON.stringify(account),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })

  const [response] = await Promise.all([request])

  return response;
}

export async function logout() {
  const request = fetch(`${uri}/account`, {
    method: 'POST',
    mode: "cors",
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })

  const [response] = await Promise.all([request])

  return response;
}