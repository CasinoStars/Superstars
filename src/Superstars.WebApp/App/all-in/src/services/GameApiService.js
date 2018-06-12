import { postAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(`${endpoint}/${gametype}`);
}

async DeleteAis() {
    return await deleteAsync(`${endpoint}/DeleteAis`);
}

async createAiUser() {
    return await postAsync(`${endpoint}/createAiUser`);
}
/*async CreateYamsGame(pot) {
    return await postAsync(`${endpoint}/CreateYamsGame`, pot);
}*/
}

export default new GameApiService();