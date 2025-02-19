<template>
    <div id="app">
      <h1>2#辅料提升机</h1>
      <div class="node" v-for="node in nodes" :key="node.id" @click="showDetails(node)">
        <p>{{ node.label }}</p>
      </div>
      <div v-if="selectedNode" class="info-modal">
        <div class="info-content">
          <h2>节点详情</h2>
          <p>温度：{{ selectedNode.temperatureC }}°C</p>
          <p>速度：{{ selectedNode.speed }} m/min</p>
          <button @click="selectedNode = null">关闭</button>
        </div>
        <div class="modal-backdrop" @click="selectedNode = null"></div>
      </div>
    </div>
  </template>
  
  <script>
  import { ref } from 'vue';
  
  export default {
    name: 'App',
    setup() {
      const nodes = ref([
        // 示例节点数据
        { id: 1, label: '节点1', temperatureC: 25, speed: 60 },
        { id: 2, label: '节点2', temperatureC: 30, speed: 55 },
        // 更多节点...
      ]);
      const selectedNode = ref(null);
  
      function showDetails(node) {
        selectedNode.value = node;
      }
  
      return { nodes, selectedNode, showDetails };
    }
  };
  </script>
  
  <style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
  
  .node {
    width: 100px;
    height: 100px;
    background-color: white;
    border: 2px solid #ccc;
    border-radius: 8px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 10px;
    transition: all 0.3s;
    cursor: pointer;
  }
  
  .node:hover {
    transform: scale(1.1);
  }
  
  .info-modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  
  .info-content {
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    position: relative;
    z-index: 2;
  }
  
  .modal-backdrop {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 1;
  }
  
  button {
    padding: 10px 20px;
    background-color: #ccc;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  button:hover {
    background-color: #aaa;
  }
  </style>
  