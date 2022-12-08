"use strict";(self.webpackChunkclient_app=self.webpackChunkclient_app||[]).push([[551],{5551:(A,m,a)=>{a.r(m),a.d(m,{CatalogueModule:()=>Z});var c=a(6895),s=a(3599),d=a(7579),h=a(2722),e=a(8256),l=a(6303),_=a(1481);let f=(()=>{class t{constructor(i){this._sanitizer=i}transform(i,...o){return this._sanitizer.bypassSecurityTrustResourceUrl(i)}}return t.\u0275fac=function(i){return new(i||t)(e.Y36(_.H7,16))},t.\u0275pipe=e.Yjl({name:"safe",type:t,pure:!0}),t})();function v(t,n){if(1&t&&(e.TgZ(0,"span",18),e._uU(1),e.qZA()),2&t){const i=n.$implicit;e.xp6(1),e.hij(" ",i," ")}}function x(t,n){if(1&t&&(e.TgZ(0,"div",1)(1,"div",2)(2,"div",3)(3,"div",4),e._UZ(4,"img",5),e.qZA()()(),e.TgZ(5,"div",6)(6,"div",2)(7,"div",7)(8,"div",8)(9,"p",9),e._uU(10),e.qZA(),e.TgZ(11,"div",10),e.YNc(12,v,2,1,"span",11),e.qZA(),e.TgZ(13,"div",12),e._UZ(14,"iframe",13),e.ALo(15,"safe"),e.qZA()()(),e.TgZ(16,"div",14)(17,"div",15)(18,"h2",16),e._uU(19),e.qZA(),e.TgZ(20,"button",17),e._uU(21),e.ALo(22,"currency"),e.qZA()()()()()()),2&t){const i=e.oxw();e.xp6(4),e.Q6J("src",i.image,e.LSH),e.xp6(6),e.Oqu(i.game.description),e.xp6(2),e.Q6J("ngForOf",i.game.category),e.xp6(2),e.Q6J("src",e.lcZ(15,6,i.youtubeLink),e.uOi),e.xp6(5),e.Oqu(i.game.title),e.xp6(2),e.hij(" ",e.lcZ(22,8,i.game.price)," ")}}let y=(()=>{class t{constructor(i,o){this._route=i,this._gameService=o,this.destroy$=new d.x,this.image="",this.youtubeLink=""}ngOnInit(){this._gameService.getGame(this._route.snapshot.params.id).pipe((0,h.R)(this.destroy$)).subscribe(o=>{this.game={...o,createdAt:new Date(o.createdAt)},o.coverArt.forEach(r=>{r.isBoxArt||(this.image=r.url)}),this.youtubeLink=`https://www.youtube.com/embed/${o.youtubeLink}`})}ngOnDestroy(){this.destroy$.next(),this.destroy$.unsubscribe()}}return t.\u0275fac=function(i){return new(i||t)(e.Y36(s.gz),e.Y36(l.h))},t.\u0275cmp=e.Xpm({type:t,selectors:[["app-game-listing"]],decls:1,vars:1,consts:[["class","container-fluid game-listing",4,"ngIf"],[1,"container-fluid","game-listing"],[1,"row"],[1,"col-12"],[1,"game-listing__container-img"],[1,"game-listing__cover",3,"src"],[1,"container","game-listing__container-content"],[1,"order-1","order-lg-0","col-12","col-lg-7","col-xl-9","game-listing__container-left"],[1,"game-listing__container-desc"],[1,"game-listing__desc"],[1,"game-listing__categories"],["class","game-listing__tags",4,"ngFor","ngForOf"],[1,"game-listing__iframe"],["title","YouTube video player","frameborder","0","allow","accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture","allowfullscreen","",3,"src"],[1,"order-0","order-lg-1","col-12","col-lg-5","col-xl-3","game-listing__container-right"],[1,"sticky-card"],[1,"sticky-card__header"],[1,"sticky-card__button","button","button-primary"],[1,"game-listing__tags"]],template:function(i,o){1&i&&e.YNc(0,x,23,10,"div",0),2&i&&e.Q6J("ngIf",o.game)},dependencies:[c.sg,c.O5,c.H9,f],styles:[".game-listing[_ngcontent-%COMP%]{margin-top:-5rem}.game-listing__container-img[_ngcontent-%COMP%]{overflow:hidden;height:100%;width:auto;display:inline-block;background-image:linear-gradient(to bottom,transparent 0%,rgba(0,0,0,.2) 100%)}.game-listing__container-content[_ngcontent-%COMP%]{margin-top:4rem;margin-bottom:4rem}.game-listing__container-left[_ngcontent-%COMP%]{padding-right:4rem}@media (max-width: 62em){.game-listing__container-left[_ngcontent-%COMP%]{padding-right:0}}.game-listing__container-right[_ngcontent-%COMP%]{margin-bottom:2rem}.game-listing__container-desc[_ngcontent-%COMP%]{display:flex;flex-flow:column;gap:4rem;height:100%;min-height:100rem}.game-listing__cover[_ngcontent-%COMP%]{width:100%;height:100%;object-fit:cover;display:block;z-index:-1;position:relative}.game-listing__iframe[_ngcontent-%COMP%]{height:100%;width:100%}.game-listing__categories[_ngcontent-%COMP%]{background-color:#3c4043;padding:2rem;border-radius:1rem;display:flex;flex-wrap:wrap;gap:2rem}.game-listing__tags[_ngcontent-%COMP%]{border-radius:1rem;background-color:#56638a;padding:1rem 2rem;text-align:center;flex:1}iframe[_ngcontent-%COMP%]{width:inherit;height:50rem}@media (max-width: 75em){iframe[_ngcontent-%COMP%]{height:40rem}}@media (max-width: 48em){iframe[_ngcontent-%COMP%]{height:35rem}}@media (max-width: 48em){iframe[_ngcontent-%COMP%]{height:30rem}}.sticky-card[_ngcontent-%COMP%]{position:sticky;top:0;display:flex;flex-flow:column;align-items:flex-end;gap:4rem;background-color:#3c4043;border-radius:1rem;padding:4rem}.sticky-card__header[_ngcontent-%COMP%]{font-size:3rem;text-align:right}@media (max-width: 75em){.sticky-card__header[_ngcontent-%COMP%]{font-size:2.5rem}}@media (max-width: 62em){.sticky-card__header[_ngcontent-%COMP%]{text-align:left}}@media (max-width: 62em){.sticky-card[_ngcontent-%COMP%]{align-items:flex-start;gap:2rem;position:relative}}"]}),t})(),C=(()=>{class t{constructor(i){this._gameService=i}resolve(i,o){return this._gameService.getAllGames()}}return t.\u0275fac=function(i){return new(i||t)(e.LFG(l.h))},t.\u0275prov=e.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})();function O(t,n){if(1&t&&(e.TgZ(0,"div",3)(1,"div",4),e._UZ(2,"img",5),e.TgZ(3,"div",6)(4,"p",7),e._uU(5),e.qZA(),e.TgZ(6,"button",8),e._uU(7," View "),e.qZA()()()()),2&t){const i=n.$implicit,o=n.index,r=e.oxw();e.xp6(2),e.Q6J("src",i,e.LSH),e.xp6(3),e.Oqu(r.games[o].title),e.xp6(1),e.MGl("routerLink","game/",r.games[o].id,"")}}const b=[{path:"",component:(()=>{class t{constructor(i,o){this._route=i,this._gameService=o,this.destroy$=new d.x,this.games=[],this.images=[]}ngOnInit(){this._route.data.subscribe(i=>{const r=i[0].map(g=>({...g,createdAt:new Date(g.createdAt)}));this._gameService.gamesList$.next(r),this.games=r;const u=[];r.forEach(g=>{g.coverArt.forEach(p=>{p.isBoxArt&&u.push(p.url)})}),this.images=u})}ngOnDestroy(){this.destroy$.next(),this.destroy$.complete()}}return t.\u0275fac=function(i){return new(i||t)(e.Y36(s.gz),e.Y36(l.h))},t.\u0275cmp=e.Xpm({type:t,selectors:[["app-catalogue"]],decls:3,vars:1,consts:[[1,"container"],[1,"row","d-flex"],["class","col-12 col-md-6 col-lg-4 col-xxl-3",4,"ngFor","ngForOf"],[1,"col-12","col-md-6","col-lg-4","col-xxl-3"],[1,"card"],[1,"card__img",3,"src"],[1,"card__content","d-flex","flex-column","align-items-end"],[1,"card__text"],[1,"button","button-primary",3,"routerLink"]],template:function(i,o){1&i&&(e.TgZ(0,"div",0)(1,"div",1),e.YNc(2,O,8,3,"div",2),e.qZA()()),2&i&&(e.xp6(2),e.Q6J("ngForOf",o.images))},dependencies:[c.sg,s.rH]}),t})(),resolve:[C]},{path:"game",redirectTo:""},{path:"game/:id",pathMatch:"full",component:y}];let w=(()=>{class t{}return t.\u0275fac=function(i){return new(i||t)},t.\u0275mod=e.oAB({type:t}),t.\u0275inj=e.cJS({imports:[s.Bz.forChild(b),s.Bz]}),t})();var M=a(7433);let Z=(()=>{class t{}return t.\u0275fac=function(i){return new(i||t)},t.\u0275mod=e.oAB({type:t}),t.\u0275inj=e.cJS({imports:[c.ez,w,M.m]}),t})()}}]);