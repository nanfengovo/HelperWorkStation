<template>
    <div class="login-container">
        <div class="Login">
        <span>
            <h2>欢迎登录</h2>
        </span>
        <div class="Login-item">
            <el-input
            v-model="UserName"
            style="width: 80%; height: 40px"
            clearable
            placeholder="请输入用户名"
            :prefix-icon="User"
            />
        </div>
        <div class="Login-item">
            <el-input
            v-model="Password"
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
import { ref } from 'vue';
import routes from '../../router/index';
import { ElNotification } from 'element-plus';
import axios from 'axios';

const UserName = ref('');
const Password = ref('');
const router = useRouter();
// const loginForm = reactive({
//     UserName: 'admin',
//     Password: '123456',
// })

// 登录模拟
const LoginClick = async() => {
        // if (loginForm.UserName === 'admin' && loginForm.Password === '123456') {
        //     ElNotification({
        //     title: '登录提示',
        //     message: '登录成功!',
        //     type: 'success',
        // });
        //         // 跳转到首页
        //         router.push('/framework');
        // } else {
        //     alert('用户名或密码错误');
        // }

        const login = {
            UserName: UserName,
            Password: Password
        };
        try {
            const response = await axios.post("http://127.0.0.1:5264/api/Admin/Login",login);
            if(response.data.code === 200) {
                ElNotification({
                    title: '登录提示',
                    message: '登录成功!',
                    type: 'success',
                });
                // 跳转到首页
                router.push('/framework');
            } else {
                ElNotification({
                    title: '登录提示',
                    message: '登录失败!',
                    type: 'warning',
                });
            }
        }
        catch (error: any) {
            ElNotification({
                    title: '登录提示',
                    message: error.message,
                    type: 'error',
                });
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