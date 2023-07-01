import Index from '@/views/Collaborator/Index'
import Create from '@/views/Collaborator/Create'
import Detail from '@/views/Collaborator/Detail'
import Edit from '@/views/Collaborator/Edit'
import Delete from '@/views/Collaborator/Delete'

export default [
  {
    path: '/collaborator',
    name: 'CollaboratorIndex',
    component: Index
  },
  {
    path: '/collaborator/create',
    name: 'CollaboratorCreate',
    component: Create
  },
  {
    path: '/collaborator/detail/:id',
    name: 'CollaboratorDetail',
    component: Detail
  },
  {
    path: '/collaborator/edit/:id',
    name: 'CollaboratorEdit',
    component: Edit
  },
  {
    path: '/collaborator/delete/:id',
    name: 'CollaboratorDelete',
    component: Delete
  }
]