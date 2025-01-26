<script setup lang="ts">
import {  RouterView } from 'vue-router'
import FrameWork from './views/FrameWork/index.vue'
import Login from './views/Login/index.vue'
</script>

<template>
  <div class="app">
    <RouterView />
  </div>
  
  <!-- <RouterView /> -->
</template>

<style scoped>
	html,body,#app{
      height: 100%;
      width: 100%;
	}
  .app{
    height: 100%;
    width: 100%;
  }
</style>
