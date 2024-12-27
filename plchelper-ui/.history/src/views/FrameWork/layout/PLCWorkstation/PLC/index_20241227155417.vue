<template>
    <div class="S7Card">
        <el-card class="card-top" shadow="hover">
            <div class="card-top-demo"> 
                
                    <div class="card-top-top">
                        <h3>PLC配置</h3>
                    </div>
                
            </div>
        </el-card>
        <el-card style="width: 100%" shadow="always">Hover</el-card>
    </div>
</template>
<script setup lang = "ts">
</script>

<style scoped>
.S7Card {
    width: 100%;
    background-color: #fbfbfb;
    border-radius: 12px; 
    display: flex;
}
.card-top {
    width: 100%;
    height: 80px;
    margin-top: 20px;
    margin-bottom: 20px;
    background-color: #fbfbfb;
    border-radius: 12px; 
}
.card-top-demo {
    display: flex;
    width: 100%;
}
.card-top-top {
    justify-content: center;
}

</style>