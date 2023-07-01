import { uri } from '@/helpers/variables'

async function getAll() {
  const [response] = await Promise.all([fetch(`${uri}/task`)])
  return await response.json()
}

async function get(id) {
  const [response] = await Promise.all([fetch(`${uri}/task/detail/${id}`)])
  const task = await response.json()
  
  console.log(task)
  return task
}

async function create(task) {
  const { name, author, performer, status, priority, comment } = task
  const request = fetch(`${uri}/task`, {
    method: 'POST',
    mode: 'cors',
    body: JSON.stringify({
      name,
      authorId: author.id,
      author,
      performerId: performer.id,
      performer,
      status,
      comment,
      priority
    }),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  }) 
  const [response] = await Promise.all([request])
  
  console.log(response)
  return response
}

async function update(task) {
  const { id, name, author, performer, status, priority, comment } = task
  const request = fetch(`${uri}/task/edit/${id}`, {
    method: 'PUT',
    mode: 'cors',
    body: JSON.stringify({
      name,
      authorId: author.id,
      author,
      performerId: performer.id,
      performer,
      status,
      comment,
      priority
    }),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })

  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

async function deleteTask(task) {
  const request = fetch(`${uri}/task/delete/${task.id}`, {
    method: 'DELETE',
    mode: 'cors',
    body: JSON.stringify(task),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    }
  })
  
  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

export { getAll, get, create, update, deleteTask }