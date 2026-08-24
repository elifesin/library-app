<script setup lang="ts">
import { ref, onMounted } from 'vue'
import CategoryService from '../services/category.service'
import type { CategoryDto } from '../models/category.dto'

const showDeleteModal = ref(false)
const categoryIdToDelete = ref<number | null>(null)

const categories = ref<CategoryDto[]>([])

const openDropdownId = ref<number | null>(null)

const toggleDropdown = (id: number) => {
  openDropdownId.value = openDropdownId.value === id ? null : id
}

const loadCategories = async () => {
  try {
    const response = await CategoryService.getAll()
    categories.value = response.data
  } catch (error) {
    console.error('Veri çekme hatası:', error)
  }
}

const confirmDelete = (id: number) => {
  categoryIdToDelete.value = id
  showDeleteModal.value = true
}

const closeModal = () => {
  showDeleteModal.value = false
  categoryIdToDelete.value = null
}

const executeDelete = async () => {
  if (categoryIdToDelete.value === null) return

  try {
    await CategoryService.Delete(categoryIdToDelete.value)

    loadCategories()
    closeModal()
  } catch (error) {
    console.error('Silme hatası:', error)
  }
}

onMounted(() => {
  loadCategories()
})
</script>

<template>
  <div>
    <div class="card m-3">
      <div class="card-body p-0">
        <table class="table table-striped">
          <thead>
            <tr>
              <th style="width: 150px">İşlemler</th>
              <th>Kategori Adı</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="category in categories" :key="category.id">
              <td>
                <div class="btn-group">
                  <button
                    @click="toggleDropdown(category.id)"
                    type="button"
                    class="btn btn-info btn-sm"
                  >
                    Seçenekler <i class="fas fa-caret-down ml-1"></i>
                  </button>

                  <div
                    class="dropdown-menu"
                    :style="{
                      display: openDropdownId === category.id ? 'block' : 'none',
                      position: 'absolute',
                      zIndex: 1000,
                    }"
                  >
                    <RouterLink :to="`/categories/edit/${category.id}`" class="dropdown-item">
                      <i class="fas fa-pen mr-2 text-primary"></i> Güncelle
                    </RouterLink>

                    <div class="dropdown-divider"></div>

                    <button @click="confirmDelete(category.id)" class="dropdown-item">
                      <i class="fas fa-trash mr-2 text-danger"></i> Sil
                    </button>
                  </div>
                </div>
              </td>
              <td class="align-middle">{{ category.categoryName }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div
      v-if="showDeleteModal"
      class="modal fade show d-block"
      tabindex="-1"
      style="background-color: rgba(0, 0, 0, 0.5)"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-danger text-white">
            <h5 class="modal-title">
              <i class="fas fa-exclamation-triangle mr-2"></i> Kategori Silme Onayı
            </h5>
            <button type="button" class="close text-white" @click="closeModal">
              <span>&times;</span>
            </button>
          </div>

          <div class="modal-body">
            <p>Bu kategoriyi silmek istediğinize emin misiniz?</p>
            <p class="text-muted"><small>Bu işlem geri alınamaz.</small></p>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="closeModal">İptal</button>
            <button type="button" class="btn btn-danger" @click="executeDelete">Evet, Sil</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
