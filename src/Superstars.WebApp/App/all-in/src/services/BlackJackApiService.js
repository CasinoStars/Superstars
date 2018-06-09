import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/blackJack";

class BlackJackApiService {
    constructor() {
    }

    async CreateJackPlayer() {
        await postAsync(`${endpoint}/CreatePlayer`);
    }

    async CreateJackAiPlayer() {
        await postAsync(`${endpoint}/CreateAi`);
    }

    async DeleteJackAiPlayer() {
        await deleteAsync(`${endpoint}/DeleteAi`);
    }
    
    async DrawCardPlayer() {
        await postAsync(`${endpoint}/PlayerDraw2Card`);
    }

    async GetPlayerCards() {
        var cards = await getAsync(`${endpoint}/GetPlayerCards`) + '';
        return cards;
    }
}

export default new BlackJackApiService();