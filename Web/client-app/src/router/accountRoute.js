import Login from '@/views/Account/Login'
import Register from '@/views/Account/Register'

export default [
  {
    path: '/account/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/account/register',
    name: 'Register',
    component: Register
  }
]