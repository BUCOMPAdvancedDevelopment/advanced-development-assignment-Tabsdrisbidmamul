"use strict";(self.webpackChunkclient_app=self.webpackChunkclient_app||[]).push([[960],{5960:(w,d,i)=>{i.r(d),i.d(d,{LoginModule:()=>_});var l=i(3599),o=i(8256),u=i(6895),n=i(433),c=i(1973),g=i(6923),f=i(9763);function p(e,s){if(1&e&&(o.TgZ(0,"div",3)(1,"div",4)(2,"span",14),o._uU(3),o.qZA()()()),2&e){const t=o.oxw();o.xp6(3),o.Oqu(t.errorMessage)}}let h=(()=>{class e{constructor(t,r,a,m){this.document=t,this._authService=r,this._router=a,this._commonService=m,this.errorMessage="",this.error=!1,this.loader=!1,this.loginForm=new n.cw({email:new n.NI("",[n.kI.required,n.kI.email]),password:new n.NI("",[n.kI.required])})}ngOnInit(){this.onChanges()}handleOnSubmit(){const{email:t,password:r}=this.loginForm.value;if(null==t||null==r)return void(this.errorMessage="Login or password is not filled in");this._commonService.loader$.next(!0);const a=new c.Mv(t,r);this.error=!1,this.errorMessage="",this.loader=!0,this.loginForm.controls.email.setErrors(null),this._authService.login(a).subscribe({next:m=>{this.loader=!1,this._router.navigate([""])},error:m=>{this.error=!0,this.errorMessage=m,this.loader=!1,this.loginForm.controls.email.setErrors({incorrect:!0}),this._commonService.loader$.next(!1)},complete:()=>this._commonService.loader$.next(!1)})}onChanges(){this.loginForm.valueChanges.subscribe(t=>{t.email.length>0?this.document.getElementById("label-email")?.classList.add("form__label-active"):this.document.getElementById("label-email")?.classList.remove("form__label-active"),t.password.length>0?this.document.getElementById("label-password")?.classList.add("form__label-active"):this.document.getElementById("label-password")?.classList.remove("form__label-active")})}}return e.\u0275fac=function(t){return new(t||e)(o.Y36(u.K0),o.Y36(g.e),o.Y36(l.F0),o.Y36(f.v))},e.\u0275cmp=o.Xpm({type:e,selectors:[["app-login-form"]],decls:23,vars:3,consts:[[1,"form",3,"formGroup","ngSubmit"],[1,"form__header"],[1,"row"],[1,"col-12"],[1,"form__group"],["type","email","id","email","formControlName","email","placeholder","Email","autocomplete","off",1,"form__input"],["id","label-email","for","email",1,"form__label"],["type","password","id","password","formControlName","password","placeholder","Password","autocomplete","off",1,"form__input"],["id","label-password","for","password",1,"form__label"],[1,"form__group","w-100","d-flex","justify-content-end"],["type","submit",1,"form__submit-button","button","button-auth","w-100",3,"disabled"],["class","col-12",4,"ngIf"],[1,"col-12","d-flex","justify-content-end"],["routerLink","/forgot-my-password",1,"form__forgot-password"],[1,"form__note","form__note--error"]],template:function(t,r){1&t&&(o.TgZ(0,"form",0),o.NdJ("ngSubmit",function(){return r.handleOnSubmit()}),o.TgZ(1,"h2",1),o._uU(2,"Login"),o.qZA(),o.TgZ(3,"div",2)(4,"div",3)(5,"div",4),o._UZ(6,"input",5),o.TgZ(7,"label",6),o._uU(8,"Email"),o.qZA()()(),o.TgZ(9,"div",3)(10,"div",4),o._UZ(11,"input",7),o.TgZ(12,"label",8),o._uU(13,"Password"),o.qZA()()(),o.TgZ(14,"div",3)(15,"div",9)(16,"button",10),o._uU(17," Submit "),o.qZA()()(),o.YNc(18,p,4,1,"div",11),o.qZA(),o.TgZ(19,"div",2)(20,"div",12)(21,"a",13),o._uU(22,"Forgot your password?"),o.qZA()()()()),2&t&&(o.Q6J("formGroup",r.loginForm),o.xp6(16),o.Q6J("disabled",!r.loginForm.valid),o.xp6(2),o.Q6J("ngIf",r.error))},dependencies:[l.yS,u.O5,n._Y,n.Fj,n.JJ,n.JL,n.sg,n.u]}),e})();const v=[{path:"",component:(()=>{class e{constructor(){}ngOnInit(){}}return e.\u0275fac=function(t){return new(t||e)},e.\u0275cmp=o.Xpm({type:e,selectors:[["app-login"]],decls:2,vars:0,consts:[[1,"container"]],template:function(t,r){1&t&&(o.TgZ(0,"div",0),o._UZ(1,"app-login-form"),o.qZA())},dependencies:[h]}),e})()}];let L=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275mod=o.oAB({type:e}),e.\u0275inj=o.cJS({imports:[l.Bz.forChild(v),l.Bz]}),e})();var b=i(7433);let _=(()=>{class e{}return e.\u0275fac=function(t){return new(t||e)},e.\u0275mod=o.oAB({type:e}),e.\u0275inj=o.cJS({imports:[L,b.m]}),e})()}}]);