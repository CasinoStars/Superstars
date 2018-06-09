import { postAsync, deleteAsync } from "../helpers/apiHelper";
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
    
    async DrawCard() {
        await deleteAsync(`${endpoint}/DrawCard`);
    }
}

export default new BlackJackApiService();