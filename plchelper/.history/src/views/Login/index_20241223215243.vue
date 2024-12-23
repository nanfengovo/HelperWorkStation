<template>
    <div class="login-container">
        <div class="Login">
        <span>
            <h2>欢迎登录</h2>
        </span>
        <div class="Login-item">
            <el-input
            v-model="loginForm.UserName"
            style="width: 80%; height: 40px"
            clearable
            placeholder="请输入用户名"
            :prefix-icon="User"
            />
        </div>
        <div class="Login-item">
            <el-input
            v-model="loginForm.Password"
            style="width: 80%; height: 40px"
            clearable
            placeholder="请输入密码"
            type="password"
            show-password
            :prefix-icon="Lock"
            />
        </div>
        <div class="Login-item">
            <el-button
            @click="LoginClick"
            type="primary"
            style="width: 80%; height: 50px"
            >登录
            </el-button>
        </div>
        </div>
    </div>
</template>


<script setup lang="ts">
import { reactive } from 'vue';
import { User, Lock, Loading } from '@element-plus/icons-vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const loginForm = reactive({
    UserName: 'admin',
    Password: '123456',
})

// 登录模拟
const LoginClick = () => {
        if (loginForm.UserName === 'admin' && loginForm.Password === '123456') {
            console.log('登录成功');
                // 跳转到首页
                router.push('/framework');
        } else {
            alert('用户名或密码错误');
        }
};

</script>

<style >
.login-container {
    background-image: url(../../assets/picture/login.png);
    height: calc(100vh);
    width: 100%;
    background-size: 100% 100%;
    display: flex;

}
.Login {
    width: 400px;
    height: 300px;
    margin: 0 auto;
    margin-top: 200px;
    border: 1px solid #ccc;
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 0 10px #ccc;
}
.Login-item {
    margin-top: 20px;
}
</style>