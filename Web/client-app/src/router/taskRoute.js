import Index from '@/views/Task/Index'
import Create from '@/views/Task/Create'

export default [
  {
    path: '/task',
    name: 'TaskIndex',
    component: Index
  },
  {
    path: '/task/create',
    name: 'TaskCreate',
    component: Create
  },
]