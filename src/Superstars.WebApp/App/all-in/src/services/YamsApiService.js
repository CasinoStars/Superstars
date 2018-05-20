import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async RollDices(model, myDices, selectedDices) {
        return await postAsync(endpoint, model, myDices, selectedDices);
    }

    async CreateYamsPlayer(model) {
        return await postAsync(`${endpoint}/createPlayer`, model)
    }
}
export default new YamsApiService();