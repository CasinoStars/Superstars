import { postAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(`${endpoint}/${gametype}`);
}

async DeleteAis(pseudo) {
    return await deleteAsync(`${endpoint}/${pseudo}/DeleteAis`);
}

async createAiUser(pseudo) {
    return await postAsync(`${endpoint}/${pseudo}/createAiUser`);
}
/*async CreateYamsGame(pot) {
    return await postAsync(`${endpoint}/CreateYamsGame`, pot);
}*/
    }

export default new GameApiService();