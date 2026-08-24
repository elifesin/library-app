<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import CategoryService from '../services/category.service'
import type { CategoryDto } from '../models/category.dto'

const route = useRoute()
const router = useRouter()

const isEditMode = ref(false)
const categoryId = ref<number | null>(null)
const isSubmitted = ref(false)
const categoryName = ref('')

onMounted(async () => {
  const idParam = route.params.id

  if (idParam) {
    isEditMode.value = true
    categoryId.value = Number(idParam)
    await loadCategory(categoryId.value)
  }
})

const loadCategory = async (id: number) => {
  try {
    const response = await CategoryService.getById(id)
    categoryName.value = response.data.categoryName || response.data.categoryName
  } catch (error) {
    console.error('Kategori getirilirken hata oluştu:', error)
  }
}

const save = async () => {
  isSubmitted.value = true

  if (categoryName.value.trim() === '') {
    return
  }

  const payload: CategoryDto = {
    id: categoryId.value || 0,
    categoryName: categoryName.value,
    isActive: true, // Varsayılan değer
  }

  try {
    if (isEditMode.value && categoryId.value) {
      await CategoryService.Update(categoryId.value, payload)
    } else {
      await CategoryService.Insert(payload)
    }
    router.push('/categories')
  } catch (error) {
    console.error('İşlem sırasında hata oluştu:', error)
  }
}

const cancel = () => {
  router.push('/categories')
}
</script>

<template>
  <div class="card m-3">
    <div class="card-header bg-primary text-white">
      <h3 class="card-title">{{ isEditMode ? 'Kategori Güncelle' : 'Yeni Kategori Ekle' }}</h3>
    </div>

    <div class="card-body">
      <form @submit.prevent="save">
        <div class="row">
          <div class="col-12">
            <div class="mb-3">
              <label class="form-label">Kategori Adı</label>
              <input
                v-model="categoryName"
                type="text"
                class="form-control"
                placeholder="Örn: Edebiyat"
              />

              <span
                v-if="isSubmitted && categoryName.trim() === ''"
                class="text-danger mt-1 d-block"
              >
                Kategori adı zorunludur.
              </span>
            </div>
          </div>
        </div>

        <div class="mt-3">
          <button type="submit" class="btn btn-primary">
            <i class="fas fa-save mr-2"></i> {{ isEditMode ? 'Güncelle' : 'Kaydet' }}
          </button>

          <button type="button" class="btn btn-secondary ml-2" @click="cancel">
            <i class="fas fa-times mr-2"></i> İptal
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
