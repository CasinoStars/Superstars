import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async RollDices(selectedDices) {
        return await postAsync(`${endpoint}/Roll`, selectedDices);
    }

    async RollIaDices(dices) {
        console.log(dices);
        return await postAsync(`${endpoint}/RollIa`, dices); 
    }

    async CreateYamsPlayer() {
        return await postAsync(`${endpoint}/createPlayer`);
    }

    async CreateYamsAiPlayer() {
        return await postAsync(`${endpoint}/createAI`);
    }

    async DeleteYamsAiPlayer() 
    {
        return await deleteAsync(`${endpoint}/deleteAI`);
    }

    async GetPlayerDices(){
        var dices = await getAsync(endpoint) + '';
        var array = dices.split('');
        return array;
    }

    async GetIaDices(){
        var dices = await getAsync(`${endpoint}/getIaDices`) + '';
        var array = dices.split('');
        return array;
    }

    async GetTurn(){
        return await getAsync(`${endpoint}/getTurn`);
    }
}
export default new YamsApiService();