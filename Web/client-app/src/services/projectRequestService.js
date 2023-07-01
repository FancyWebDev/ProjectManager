import { uri } from '@/helpers/variables'

async function getAll() {
  const [response] = await Promise.all([fetch(`${uri}/project`)])
  const projects = await response.json()

  console.log(projects)
  return projects
}

async function get(id) {
  const [response] = await Promise.all([fetch(`${uri}/project/detail/${id}`)])
  return await response.json()
}

async function create(project) {
  let formData = new FormData()

  for (const name in project){
    if(typeof project[name] === 'string'){
      formData.append(name, project[name])
    }
    else{
      formData.append(name, JSON.stringify(project[name]))
    }
  }

  formData.append('files', project.files)

  const request = fetch(`${uri}/project`, {
    method: 'POST',
    mode: 'cors',
    body: formData,
  })

  const [response] = await Promise.all([request])
  console.log(response)
  return response
}

async function update(project) {
  const request = fetch(`${uri}/project/edit/${project.id}`, {
    method: 'PUT',
    mode: 'cors',
    body: JSON.stringify(project),
    headers: {
      'Content-type': 'application/json; charset=UTF-8'
    }
  })

  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

async function deleteProject(project) {
  const request = fetch(`${uri}/project/delete/${project.id}`, {
    method: 'DELETE',
    mode: 'cors',
    body: JSON.stringify(project),
    headers: {
      'Content-type': 'application/json; charset=UTF-8'
    }
  })

  const [response] = await Promise.all([request])

  console.log(response)
  return response
}

export { getAll, get, create, update, deleteProject }