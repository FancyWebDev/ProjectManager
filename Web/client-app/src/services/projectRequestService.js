import { uri } from '@/helpers/variables'

export async function getAll() {
  const [response] = await Promise.all([fetch(`${uri}/project`)])
  return await response.json()
}

export async function get(id) {
  const [response] = await Promise.all([fetch(`${uri}/project/detail/${id}`)])
  return await response.json()
}

export async function create(project) {
  const formData = new FormData()
  const {
    name,
    customerCompanyName,
    performerCompanyName,
    directorId,
    collaboratorsId,
    priority,
    projectStartDate,
    projectEndDate,
    files
  } = project

  formData.append('name', name)
  formData.append('customerCompanyName', customerCompanyName)
  formData.append('performerCompanyName', performerCompanyName)
  formData.append('directorId', directorId)
  formData.append('priority', priority)
  formData.append('projectStartDate', projectStartDate)
  formData.append('projectEndDate', projectEndDate)
  appendArray(collaboratorsId, 'collaboratorsId')
  appendArray(files, 'files')

  function appendArray(array, key){
    for (let i = 0; i < array.length; i++) {
      formData.append(key, array[i])
    }
  }

  const request = fetch(`${uri}/project`, {
    method: 'POST',
    mode: 'cors',
    body: formData,
  })

  formData.forEach(value => {
    console.log(value)
  })

  const [response] = await Promise.all([request])
  console.log(response)
  return response
}

export async function update(project) {
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

export async function deleteProject(project) {
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