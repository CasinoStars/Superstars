<template>
    <div class="container">

        <form @submit="onSubmit($event)">
            <!-- <div class="alert aler-danger" v-if="errors.length > 0">
                <b>Les champs suivants sont invalides : </b>
                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>          -->

            <div class="form-group">
                <label class="required">Pseudo : </label>
                <input type="text" class="form-control" required>
            </div>

        <div class="form-group">
            <label class="required">Mot de passe : </label>
            <input type="text" class="form-control" required>
            <span asp-validation-for="Password"></span>
        </div>

        <div class="form-group">
            <button type="submit" class="btn btn-dark btn-block">S'inscrire</button>
        </div>
        </form>
</div>
  
</template>
<script>
    import { mapActions } from 'vuex'
    //import AuthService from '../services/UserApiService'

    export default {
    //     data () {
    //         return {
    //             item: {},
    //             mode: null,
    //             id: null,
    //             errors: []
    //         }
    //     },

    //     async mounted() {
    //         this.mode = this.$route.params.mode;
    //         this.id = this.$route.params.id;

    //         try {
    //             this.item = await this.executeAsyncRequest(() => UserApiService.login());
    //         }
    //         catch(error) {
    //             this.$router.replace('/home');
    //         }
    //     },

    //     methods: {
    //         //...mapActions(['executeAsyncRequest']),

    //         async onSubmit(e) {
    //             e.preventDefault();

    //             var errors = [];

    //             if(!this.item.Pseudo) errors.push("Pseudo")
    //             if(!this.item.Password) errors.push("Password")

    //             this.errors = errors;

    //             if(errors.length == 0) {
    //                 try {
    //                     await this.executeAsyncRequest(() => UserApiService.register(this.item));
    //                     this.$router.replace('/home');
    //                 }
    //                 catch(error) {
    //                 }
    //             }
    //         }
    //     }
    }
</script>