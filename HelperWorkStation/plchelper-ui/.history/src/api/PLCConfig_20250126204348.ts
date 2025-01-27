import axios from "axios";

const apiClient = axios.create({
    baseURL: 'http:127.0.0.1:5264/api/PLCConfig', // 替换为你的后端地址
    headers: {
        'Content-Type': 'application/json',
    },
});
export const savePLCConfig = async (config: { inputIP: string; inputPort: number }) => {
    try {
        const response = await apiClient.post('/PLCConfig', config);
        return response.data;
    } catch (error) {
        console.error('Error saving PLC config:', error);
        throw error;
    }
};