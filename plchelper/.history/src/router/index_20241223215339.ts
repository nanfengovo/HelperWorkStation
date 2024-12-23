import { createMemoryHistory, createRouter, createWebHashHistory } from 'vue-router'
import frameWork from '../views/FrameWork/index.vue';
import Login from '../views/Login/index.vue';

const routes = [
    { path: '/', component: Login },
    { path: '/framework', component: frameWork },
    ]


    const router = createRouter({
        history: createWebHashHistory(),
        routes,
    })

export default router;