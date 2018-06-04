import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async RollDices(selectedDices) {
        return await postAsync(`${endpoint}/Roll`, selectedDices);
    }

    async createAIYams(dices) {
        console.log(dices);
        return await postAsync(`${endpoint}/CreateAIYams`, dices); 
    }

    async CreateYamsPlayer() {
        return await postAsync(`${endpoint}/createPlayer`);
    }

    async CreateYamsAiPlayer(pseudo) {
        return await postAsync(`${endpoint}/${pseudo}/createAI`);
    }

    async DeleteYamsAiPlayer(pseudo) 
    {
        return await deleteAsync(`${endpoint}/${pseudo}/deleteAI`);
    }

    async GetPlayerDices(){
        var dices = await getAsync(endpoint) + '';
        var array = dices.split('');
        return array;
    }

    async GetTurn(){
        return await getAsync(`${endpoint}/getTurn`);
    }
}
export default new YamsApiService();