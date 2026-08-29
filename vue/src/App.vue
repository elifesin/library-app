<script setup lang="ts">
import Sidebar from '@/shared/layout/sidebar/sidebar.vue'
import Header from '@/shared/layout/header/header.vue'
import Footer from '@/shared/layout/footer/footer.vue'
</script>

<template>
  <div class="wrapper">
    <Header />

    <Sidebar />

    <div class="content-wrapper">
      <section class="content">
        <div class="container-fluid pt-3">
          <RouterView />
        </div>
      </section>
    </div>

    <Footer />
  </div>
</template>

<style scoped></style>
