<template>
    <div v-for="item in list" class="box" >{{item}}</div>
</template>
<script setup lang = "ts">
import { ref } from 'vue';

const list = ref(['1-1','1-2','1-3','1-4'])

</script>
<style>
.box{
    width: 150px;
    height: 150px;
    margin: 50px 0 0 400px;
    font-size: 24px;
    font-weight: 700px;
    text-align: center;
    line-height: 150px;
    background-color: antiquewhite;
}
</style>
