<template>
    <div class="common-layout">
        <el-container>
            <el-header>
                Header
            </el-header>
            <el-container>
                <el-aside width="200px" style="background-color: " > 
                    <el-radio-group v-model="isCollapse" style="margin-bottom: 20px">
                        <!-- <el-radio-button :value="false">expand</el-radio-button> -->
                        <el-radio-button :value="true"><el-icon><Fold /></el-icon></el-radio-button>
                    </el-radio-group>
                    <el-menu
                        height="100vh"
                        background-color="#d0ffff"
                        default-active="2"
                        class="el-menu-vertical-demo"
                        :collapse="isCollapse"
                        @open="handleOpen"
                        @close="handleClose"
                    >
                        <el-menu-item index="1">
                            <el-icon><location /></el-icon>
                            <template #title>Navigator Two</template>
                        </el-menu-item>
                        <el-menu-item index="2">
                            <el-icon><icon-menu /></el-icon>
                            <template #title>Navigator Two</template>
                        </el-menu-item>
                        <el-menu-item index="3" disabled>
                        <el-icon><document /></el-icon>
                        <template #title>Navigator Three</template>
                        </el-menu-item>
                        <el-menu-item index="4">
                        <el-icon><setting /></el-icon>
                        <template #title>Navigator Four</template>
                        </el-menu-item> 
                                    </el-menu>
            </el-aside>
                <el-container>
                    <el-main>Main</el-main>
                    <el-footer>Footer</el-footer>
                </el-container>
            </el-container>
        </el-container>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import {
    Document,
    Menu as IconMenu,
    Location,
    Setting,
} from '@element-plus/icons-vue'

const isCollapse = ref(true)
const handleOpen = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}
const handleClose = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}
</script>

<style>
.el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;
    min-height: 100vh;
}

</style>