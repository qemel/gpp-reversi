## Game Programming Practice

ゲームのプログラミングを練習するシリーズです。

### Reversi

リバーシです。

#### 完成イメージ

![reversi](https://github.com/qemel/gpp-reversi/assets/99727161/bc42d211-837a-40ec-b7ee-5f9131eaa832)


### 設計の方針

- Domain, Application, Presentationの3層構成
  - Domainにはドメインロジックを書く
  - ApplicationはDomainを利用し、IViewを中継してPresentation層とつなぎこむ
  - PresentationはIViewを継承したMonoBehaviourクラスを使い、Unity上に表示したり入力を受け取ったりする
 
- できるだけ状態を減らす
  - GamePresenterにすべてのゲームループを記述
  - リバーシくらい小さなゲームだからできるわけであって、普通のゲームではまずできない（もっとクラスが必要）と思った
 
### 使用プラグインなど

- NugetForUnity
- CsProjModifier
- R3
- UniTask
- VContainer
