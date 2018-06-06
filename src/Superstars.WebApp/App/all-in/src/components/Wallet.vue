<template>
    <div id="wallet">
        <!-- errors of transactions !-->
        <div class="alert alert-danger" style="text-align: center;" v-if="errors.length > 0">
            <li v-for="e of errors" :key="e">{{e}}</li>
        </div>

        <!-- change view wallet real/virutal !-->
        <div style="text-align: center; margin-top: 2%;">
            <div class="btn-group" v-if="this.wallet == 'fake'">
                <button type="button" v-on:click="ChangeWallet('real')" class="btn btn-secondary">Real wallet</button>
            </div>
            <div class="btn-group" v-else>
                <button type="button" v-on:click="ChangeWallet('fake')" class="btn btn-secondary">Fake wallet</button>
            </div>
        </div>

        <!-- REALLY WALLET HERE -->
        <div style="text-align: center;" v-if="this.wallet == 'real'">
            <h1> Adresse de dépots BTC: {{BTCAddress}} </h1>
            <h1>Solde du compte réel: {{trueCoins}} </h1>
        </div>

        <!-- FAKE WALLET HERE -->
        <div v-if="this.wallet == 'fake'">
            <h1 style="text-align: center">Solde du compte virtuel: {{fakeCoins.balance}} all'in</h1>
            <form @submit="onSubmit($event)" :v-model="item.moneyType = 2">
            <div class="form-group">
                <label style="text-align: left;">Montant :</label>
                <input type="number" min="0" max="1000000" placeholder="..." v-model="item.fakeCoins" class="form-control" required>
            </div>
            <button type="submit" class="btn btn-primary">Créditer</button>
            </form>
        </div>

    </div>
</template>

<style lang="css">
  
</style>

<script>
import { mapActions } from 'vuex';
import WalletApiService from '../services/WalletApiService';

export default {
    data(){
        return {
            item: {},
            wallet: '',
            fakeCoins: 0,
            trueCoins: 'waiting...',
            BTCAddress: '',
            errors: []
        };
    },

    mounted(){
        this.wallet = 'real';
        this.refreshTrueCoins();
        this.refreshFakeCoins();
        this.GetWalletAddress();
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async GetWalletAddress(){
          this.BTCAddress = await this.executeAsyncRequest(() => WalletApiService.GetWalletAddress());
          console.log(this.BTCAddress);
        },

        async refreshFakeCoins(){    
            this.fakeCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBalance());
        },

        async refreshTrueCoins(){
            this.trueCoins = await this.executeAsyncRequest(() => WalletApiService.GetTrueBalance());
        },


        async onSubmit(e) {
            e.preventDefault();

                var errors = [];
                if(this.fakeCoins.balance >= 1000000)
                    errors.push("Le crédit est bloquer lorsque votre solde atteind 1,000,000 de bits");           
                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        await this.executeAsyncRequest(() => WalletApiService.AddCoins(this.item));
                        await this.refreshFakeCoins();
                    }
                    catch(error) {
                    }
                }
        },

        ChangeWallet(wallet){
            this.wallet = wallet;
            this.refreshFakeCoins();
            this.errors = 0;           
        },
    }
}
</script>

