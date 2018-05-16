import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor() {
        
    }

async RollDices(selectedDices) {
    return await postAsync(endpoint, selectedDices);
}
    }
export default new YamsApiService();