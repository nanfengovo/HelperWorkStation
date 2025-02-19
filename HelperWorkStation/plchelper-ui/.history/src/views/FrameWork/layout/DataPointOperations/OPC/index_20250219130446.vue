<template>
    <div @click="IsBig=!IsBig"  :class="IsBig?'big':'small'"></div>
</template>
<script setup lang = "ts">
import { ref } from 'vue';

const IsBig = ref(false)
</script>
<style >
.big{
    width: 200px;
    height: 200px;
    background-color:antiquewhite;
    margin: 200px 0 0 400px;
}
.small{
    width: 50px;
    height: 50px;
    background-color:burlywood;
    margin: 200px 0 0 400px
}
</style>