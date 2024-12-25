import { createMemoryHistory, createRouter } from 'vue-router'
import frameWork from '../views/FrameWork/index.vue';
import Login from '../views/Login/index.vue';

const routes = [
    { path: '/', component: Login },
    { path: '/about', component: frameWork },
    ]


    const router = createRouter({
        history: createMemoryHistory(),
        routes,
    })

    export default router;