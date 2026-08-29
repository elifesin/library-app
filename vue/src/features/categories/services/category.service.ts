import axios from 'axios'
import type { CategoryDto } from '../models/category.dto'

const api_url = 'http://localhost:5147/api/categories'

export default {
  getAll() {
    return axios.get<CategoryDto[]>(api_url)
  },

  getById(id: number) {
    return axios.get<CategoryDto>(`${api_url}/${id}`)
  },

  Insert(category: CategoryDto) {
    return axios.post(api_url, category)
  },

  Update(id: number, category: CategoryDto) {
    return axios.put(`${api_url}/${id}`, category)
  },

  Delete(id: number) {
    return axios.delete(`${api_url}/${id}`)
  },
}
