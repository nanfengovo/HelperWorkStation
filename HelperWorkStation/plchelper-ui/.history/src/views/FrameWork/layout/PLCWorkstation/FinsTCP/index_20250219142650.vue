<template>
    <div class="storage-layout">
      <div v-for="item in items" :key="item.id" class="item">{{ item.label }}</div>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  
  const items = ref([
    { id: 1, label: '3' },
    { id: 2, label: '4' },
    { id: 3, label: '5' },
    { id: 4, label: '22' },
    { id: 5, label: '23' },
    { id: 6, label: '33' },
    { id: 7, label: '34' }
  ]);
  </script>
  
  <style scoped>
  .storage-layout {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-template-rows: repeat(3, 1fr);
    gap: 10px;
  }
  
  .item {
    display: flex;
    align-items: center;
    justify-content: center;
    border: 1px solid black;
    height: 100%;
  }
  </style>
  