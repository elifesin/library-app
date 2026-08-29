import { createRouter, createWebHistory } from 'vue-router'
import CategoryComponent from '@/features/categories/pages/category.index.vue'
import categoryCreateEdit from '@/features/categories/pages/category.create-edit.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/categories',
      name: 'categoryIndex',
      component: CategoryComponent,
    },
    {
      path: '/categories/create',
      name: 'categoryCreate',
      component: categoryCreateEdit,
    },
    {
      path: '/categories/edit/:id',
      name: 'categoryEdit',
      component: categoryCreateEdit,
    },
  ],
})

export default router
