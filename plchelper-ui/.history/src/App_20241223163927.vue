<script setup lang="ts">
import {  RouterView } from 'vue-router'
import FrameWork from './views/FrameWork/index.vue'
</script>

<template>
  <div class="app">
    <FrameWork/>
  </div>
  
  <!-- <RouterView /> -->
</template>

<style scoped>
.app{
  height: 100vh;
  width: 100vw;
}
</style>
