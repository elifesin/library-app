<script setup lang="ts">
import { ref } from 'vue'
import { RouterLink } from 'vue-router'

const isCategoryMenuOpen = ref(false)

const toggleCategoryMenu = () => {
  isCategoryMenuOpen.value = !isCategoryMenuOpen.value
}
</script>

<template>
  <aside class="main-sidebar sidebar-dark-primary elevation-4">
    <RouterLink to="/" class="brand-link">
      <i class="fas fa-university ml-3 mr-2"></i>
      <span class="brand-text font-weight-light" style="font-size: 16px"
        ><b>EBS - Eğitim Bilgi Sistemi</b></span
      >
    </RouterLink>

    <div class="sidebar">
      <div class="form-inline mt-3">
        <div class="input-group" data-widget="sidebar-search">
          <input
            class="form-control form-control-sidebar"
            type="search"
            placeholder="Search"
            aria-label="Search"
          />
          <div class="input-group-append">
            <button class="btn btn-sidebar">
              <i class="fas fa-search fa-fw"></i>
            </button>
          </div>
        </div>
      </div>

      <nav class="mt-2">
        <ul class="nav nav-pills nav-sidebar flex-column" role="menu" data-accordion="false">
          <li class="nav-item menu-open">
            <a href="#" class="nav-link active">
              <i class="nav-icon fas fa-book-reader"></i>
              <p>
                Kütüphane Sistemi
                <i class="right fas fa-angle-left"></i>
              </p>
            </a>
          </li>

          <li class="nav-item" :class="{ 'menu-open': isCategoryMenuOpen }">
            <a href="#" class="nav-link" @click.prevent="toggleCategoryMenu">
              <i class="nav-icon fas fa-table"></i>
              <p>
                Kategori İşlemleri
                <i class="right fas fa-angle-left"></i>
              </p>
            </a>

            <ul
              class="nav nav-treeview"
              :style="{ display: isCategoryMenuOpen ? 'block' : 'none' }"
            >
              <li class="nav-item">
                <RouterLink to="/categories" exact-active-class="active" class="nav-link">
                  <i class="far fa-circle nav-icon"></i>
                  <p>Kategorileri Listele</p>
                </RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/categories/create" active-class="active" class="nav-link">
                  <i class="far fa-circle nav-icon"></i>
                  <p>Yeni Kategori Ekle</p>
                </RouterLink>
              </li>
            </ul>
          </li>
        </ul>
      </nav>
    </div>
  </aside>
</template>
