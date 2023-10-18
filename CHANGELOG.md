# [0.2.0](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.1.0...v0.2.0) (2023-10-18)


### Bug Fixes

* **motor:** crash due to DCMotor initialization. ([471acdb](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/471acdb5af939b204b8f98076ef297a1b59b844d))
* **motor:** fix jitter due to double activation ([07a01dc](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/07a01dcc5a32846b21ebed006ff961e3fc6fedbd))
* **motor:** update speed if motorOutput exists ([a8cbcfa](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a8cbcfa4b1c0802283513bb7512f889417f0b7ac))


### Features

* **motor:** Add FirstOrderTF ([f317868](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f3178681f076d4d6d513de1cb84b653d5683faec))
* **motor:** add Integrator and StateSpace classes ([bc49551](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/bc4955170f8475eb2cc1b9b9fe62e9b40afd0079))
* **motor:** add motor interface ([e6b821f](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/e6b821fb6ccd916a57131c55d98cb6e2ffeb5a6a))
* **motor:** add test framework for integrators ([8d01117](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8d01117eb4d5ca116f6c52195fb05fd5d2cbea16))
* **motor:** change StateSpace to use delegate ([bf856c9](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/bf856c94365e2a698fb9cace263205b528797415))
* **motor:** implement class for linear state spaces ([cf6ae80](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/cf6ae802e5ac1babcf9848043b00bad551c20624))
* **motor:** implement DCMotor ([3592bf1](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3592bf12d8f922ae6f61ba84296a240c5ca15394))
* **motor:** implement PMSM ([73cac1b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/73cac1b8523f167e332a75d6c1aa290121bd9e8c))
* **motors:** add lineargrowth and sine integrator tests ([b281172](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/b281172090d309d61b526cc47d4d959d5c576a5e))
* **motors:** add tests for Heun and RK4 ([33538aa](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/33538aaa0ee44d52fbea05f82fcfe33d4f89ee63))
* **motor:** use 1st-order TF. ([6fbeeb1](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/6fbeeb1a3f9a29e8f9fdffcd79e8438f280b96ae))



# [0.1.0](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.0.2...v0.1.0) (2023-10-16)


### Bug Fixes

* **quadcopter:** comments and animation ([7bcf822](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/7bcf8221d152c43199aa5fef75c7e06007f09a6c))
* **quadcopter:** update gitignore ([9e03d43](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/9e03d4383cccbd3d1355957116a19f0a3fee7a48))
* **quadcopter:** use quaternion calculation for propeller animation ([f54a285](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f54a285613ce476da010fdb27344f4949d29aaa1))
* **quadcopter:** vscode gitignore ([31dd4be](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/31dd4be4e6bcab92c3c859ef07a89b189afdd272))


### Features

* **dynamics:** add dynamics interface ([3cadb8f](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3cadb8fd14a3df8686ad807d59960187bc6beb4f))
* **dynamics:** add RigidBody and test Forces ([defc719](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/defc719196ea09036ec20a65ad231edd2c617f8c))
* **eigen3:** add 2D array constructor ([23d607c](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/23d607c2b1e8e5c95e02b2e028aa06cfef6030af))
* **eigen3:** add and subtract operations ([ae8b3fe](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/ae8b3fe5913a83a170583881e403a9c2a77db9cc))
* **eigen3:** add conversion to float[,] ([16b8821](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/16b882196df9bd76833db7aaeae4d7d292dbc19d))
* **eigen3:** add equality check ([0c28743](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0c287431f2cc74dde096b1415ab486c0a5da860f))
* **eigen3:** add eye constructor ([388b5cf](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/388b5cfd093bfaeb0a7d5c05f712692a4a2a3153))
* **eigen3:** add inverse property ([d62cc11](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/d62cc11ff80ff03fdaff544ca98bb43ff3bfedad))
* **eigen3:** add transpose operations ([0561b15](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0561b1582a668f02279ff767a62d59cb899e18b4))
* **eigen3:** split tests and add copy constructor ([23b2516](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/23b25163a4b24742148c331b027f3fd9bd85941f))
* **eigen3:** support scalar multiplication ([a1c55c6](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a1c55c6454ed593d247ba5c8dbde4c478271bfaf))
* **eigen3:** tests for ToArray ([78438d2](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/78438d2b3f8b37f19600fbeff1e39e544b874bce))
* **eigen:** add basic matrix functions ([3dc39c3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3dc39c398c21fdd78d963293f3e4ff8492012a70))
* **eigen:** add column and row-major 1D constructor ([3a8687e](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3a8687e98055c54fb6c04094756e58a227b836b5))
* **quadcopter:** Add 3d model ([413fab6](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/413fab6935a115814d6cdd9509521718879a87a8))
* **quadcopter:** add propeller interface ([942c2b9](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/942c2b910513940f5d7a06044760361f23448d45))
* **quadcopter:** implement propeller function ([9994438](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/99944386d3f25399b8ef2738f7f91f7d7d4cc98d))
* **quadcopter:** implement simplepropeller ([d3dec6b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/d3dec6b8a5f22f13249c71a411e3a38b901a7f2d))
* **test:** add unit tests and code coverage ([b58212d](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/b58212d2aea6110398927e233be7053fff2b7759))



## [0.0.2](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.0.1...v0.0.2) (2023-10-06)



## 0.0.1 (2023-10-06)



