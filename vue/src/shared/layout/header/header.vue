<script setup lang="ts">
const toggleSidebar = () => {
  document.body.classList.toggle('sidebar-collapse')
}
</script>

<template>
  <nav class="main-header navbar navbar-expand navbar-white navbar-light">
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" href="#" role="button" @click.prevent="toggleSidebar">
          <i class="fas fa-bars"></i>
        </a>
      </li>
    </ul>

    <h3
      style="color: rgb(107, 106, 106); font-size: 25px; margin-left: 15px; margin-bottom: 0"
    ></h3>
  </nav>
</template>
