<script setup lang="ts">
const currentYear: number = new Date().getFullYear()
</script>

<template>
  <footer class="main-footer">
    <div class="float-right d-none d-sm-block"><b>Version</b> 1.0.0</div>

    <strong>Copyright &copy; {{ currentYear }} <a href="#">EBS</a>.</strong> Tüm hakları saklıdır.
  </footer>
</template>
