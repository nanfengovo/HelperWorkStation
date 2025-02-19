<template>
    <div class="container">
        <div v-for="item in list" class="box" >{{item}}</div>
    </div>
    
</template>
<script setup lang = "ts">
import { ref } from 'vue';

const list = ref(['1-1','1-2','1-3','1-4','2-1','2-2','2-3','2-4','3-1','3-2','3-3','3-4','4-1','4-2','4-3','4-4'])

</script>
<style>
.box{
    width: 70px;
    height: 70px;
    margin: 50px 0 0 20px;
    font-size: 24px;
    font-weight: 700px;
    line-height: 150px;
    background-color: antiquewhite;
    display: flex; /* 使用 flex 布局 */
    justify-content: center; /* 水平居中 */
    align-items: center; /* 垂直居中 */
}
.container {
    display: flex; /* 设置为 Flexbox 容器 */
    flex-wrap: wrap; /* 允许子元素换行 */
    justify-content: space-between; /* 平均分布子元素，或者你可以选择 flex-start, flex-end, center 等 */
}
</style>
