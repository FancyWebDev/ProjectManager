import Index from '@/views/Project/Index'
import Create from '@/views/Project/Create'
import Edit from '@/views/Project/Edit'
import Detail from '@/views/Project/Detail'
import Delete from '@/views/Project/Delete'
import Wizard from '@/views/Project/Wizard/Wizard'

export default [
  {
    path: '/project',
    name: 'ProjectIndex',
    component: Index
  },
  {
    path: '/project/create',
    name: 'ProjectCreate',
    component: Create
  },
  {
    path: '/project/edit/:id',
    name: 'ProjectEdit',
    component: Edit
  },
  {
    path: '/project/detail/:id',
    name: 'ProjectDetail',
    component: Detail
  },
  {
    path: '/project/delete/:id',
    name: 'ProjectDelete',
    component: Delete
  },
  {
    path: '/project/wizard',
    name: 'ProjectWizard',
    component: Wizard
  }
]