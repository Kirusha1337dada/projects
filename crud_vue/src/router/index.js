import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/paintings',
    name: 'Paintings',
    component: () => import('@/views/PaintingsPage')
  },
  {
    path: '/halls',
    name: 'Halls',
    component: () => import('@/views/HallsPage'),
  },
  {
    path: '/painting-edit/:id?',
    name: 'PaintingEdit',
    props: (route) => {
      return {
        id: route.params.id,
      }
    },
    component: () => import('@/views/PaintingEdit'),
  },
  {
    path: '/hall-edit/:id?',
    name: 'HallEdit',
    props: (route) => {
      return {
        id: route.params.id,
      }
    },
    component: () => import('@/views/HallEdit'),
  },
  {
    path: '/:catchAll(.*)',
    name: 'NotFound',
    component: () => import('@/views/PaintingsPage'),
  },
  {
    path: '/hall-paintings',
    name: 'HallPaintings',
    props: (route) =>{
      return{
        id: route.params.id,
      }
    },
    component: () => import('@/views/HallPaintingsPage')
  },
  
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
