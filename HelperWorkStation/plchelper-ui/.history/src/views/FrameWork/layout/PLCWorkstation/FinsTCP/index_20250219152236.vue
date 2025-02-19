<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->
<template>
    <div class="min-h-screen bg-gray-50 p-8">
    <!-- 标题区域 -->
    <div class="mb-8 text-center">
    <h1 class="text-3xl font-bold text-gray-800">2#辅料提升机</h1>
    </div>
    <!-- 提升机主体区域 -->
    <div class="mx-auto w-[800px]">
    <!-- 输送线段展示区域 -->
    <div class="relative">
    <!-- 5x5网格 -->
    <div class="grid grid-cols-5 gap-4">
    <template v-for="i in 25" :key="i">
    <div v-if="[3,4,5,8,13,14,15,18,21,22,23].includes(i)"
    class="transport-box cursor-pointer"
    :class="{ 'active': activeBox === i.toString() }"
    @click="handleBoxClick(i.toString())"
    >
    {{ i }}
    </div>
    <div v-else class="transport-box opacity-0 pointer-events-none">
    {{ i }}
    </div>
    </template>
    </div>
    </div>
    </div>
    <!-- 详情弹窗 -->
    <el-dialog
    v-model="dialogVisible"
    :title="`输送线段 ${selectedBox} 详情`"
    width="30%"
    >
    <div class="p-4">
    <p class="mb-2"><span class="font-bold">线段编号:</span> {{ selectedBox }}</p>
    <p class="mb-2"><span class="font-bold">运行状态:</span> {{ getStatus() }}</p>
    <p class="mb-2"><span class="font-bold">运行速度:</span> {{ getSpeed() }} m/s</p>
    <p class="mb-2"><span class="font-bold">载重情况:</span> {{ getLoad() }}</p>
    </div>
    </el-dialog>
    </div>
    </template>
    <script lang="ts" setup>
    import { ref } from 'vue';
    import { ElDialog } from 'element-plus';
    const activeBox = ref('');
    const selectedBox = ref('');
    const dialogVisible = ref(false);
    const handleBoxClick = (boxId: string) => {
    activeBox.value = boxId;
    selectedBox.value = boxId;
    dialogVisible.value = true;
    };
    const getStatus = () => {
    const statuses = ['正常运行', '待机中', '检修中'];
    return statuses[Math.floor(Math.random() * statuses.length)];
    };
    const getSpeed = () => {
    return (Math.random() * 2 + 0.5).toFixed(2);
    };
    const getLoad = () => {
    return `${Math.floor(Math.random() * 50 + 50)}%`;
    };
    </script>
    <style scoped>
    .transport-box {
    width: 80px;
    height: 80px;
    border: 2px solid #1f2937;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 24px;
    font-weight: bold;
    background-color: white;
    transition: all 0.3s ease;
    }
    .transport-box.active {
    background-color: #e5e7eb;
    box-shadow: 0 0 0 2px #4b5563;
    }
    .transport-box:hover {
    background-color: #f3f4f6;
    transform: scale(1.05);
    }
    </style>
    