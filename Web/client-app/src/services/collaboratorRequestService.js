import { uri } from '@/helpers/variables'

async function getAll() {
  const [response] = await Promise.all([fetch(`${uri}/collaborator`)])
  return await response.json()
}

async function get(id) {
  const [response] = await Promise.all([fetch(`${uri}/collaborator/detail/${id}`)])
  const collaborator = await response.json()
  
  console.log(collaborator)
  return collaborator
}

async function update(collaborator) {
  const request = fetch(`${uri}/collaborator/edit/${collaborator.id}`, {
    method: 'PUT',
    mode: 'cors',
    body: JSON.stringify(collaborator),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })

  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

async function deleteCollaborator(collaborator) {
  const request = fetch(`${uri}/collaborator/delete/${collaborator.id}`, {
    method: 'DELETE',
    mode: 'cors',
    body: JSON.stringify(collaborator),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })
  
  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

export { getAll, get, update, deleteCollaborator }