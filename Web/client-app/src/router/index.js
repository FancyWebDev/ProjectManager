import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home'
import projectRoute from './projectRoute'
import collaboratorRoute from './collaboratorRoute'
import taskRoute from './taskRoute'
import accountRoute from './accountRoute'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  ...projectRoute,
  ...collaboratorRoute,
  ...taskRoute,
  ...accountRoute
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router