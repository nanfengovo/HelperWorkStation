// import './assets/main.css'

import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import { createPinia } from 'pinia'

import App from './App.vue'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
//element-plus转换中文
import locale from 'element-plus/lib/locale/lang/zh-cn'
import router from './router'

const app = createApp(App)
app.use(router)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
    }
app.use(ElementPlus)
app.use(createPinia())


app.mount('#app')
