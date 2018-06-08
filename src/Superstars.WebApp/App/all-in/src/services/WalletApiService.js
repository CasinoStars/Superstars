import { postAsync, getAsync, getAsyncNoJSON } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(model) {
        return await postAsync(endpoint, model);
    }

    async GetFakeBalance(){
        return await getAsync(`${endpoint}/FakeBalance`);
    }

    async GetTrueBalance(){
        return await getAsync(`${endpoint}/TrueBalance`);
    }

    async GetWalletAddress(){
        return  await getAsyncNoJSON(`${endpoint}/GetAddress`);
    }

    async Withdraw(model){
        return await postAsync(`${endpoint}/Withdraw`, model);
    }
}

export default new WalletApiService();