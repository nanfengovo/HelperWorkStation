import axios from "axios";

export const savePLCConfig = async () => {
    const res = await axios.post("127.0.0.1:5264/api/PLCConfig");
    return res.data;
    };  