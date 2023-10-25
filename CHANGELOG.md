## [0.3.1](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.3.0...v0.3.1) (2023-10-25)


### Bug Fixes

* **dynamics:** actuators function without comSystem. ([0fe1339](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0fe133945e29a3a1e7fe4b3103cf5c6f9e36066f))
* **eigen3:** add exceptions for matrix operations ([f1cb9d6](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f1cb9d64c0ac7103b010a4b5f750469ac6e2f109))
* **eigen3:** add exceptions for matrix properties ([27e78b3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/27e78b320ee49edb2fc914099c1a56408371a371))
* **propeller:** use Func<float[]> instead. ([50e76e0](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/50e76e0c1694dbfd7bd0e2591401f1925197ab09))



# [0.3.0](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.2.0...v0.3.0) (2023-10-23)


### Bug Fixes

* **communication:** fix link errors with the ROS2ForUnity plugin. ([adf27b6](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/adf27b6f2b3568d406d6dcd9898f3785097fce7f))
* **quadcopter:** remove FixedUpdate in RigidBody. ([bd69ad0](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/bd69ad0ec8bfe3fa5e4e4d112fe79ed87e6f2185))
* **quadcopter:** use xzy for 6DOF motion measurement. ([2ddd35c](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/2ddd35c468d353b61db14e270b6f16a0d9c9b1af))


### Features

* **communication:** add ActuatorSystem ([f57e01f](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f57e01f216c4d0fd9e1d7716e2e0776a2fc984d9))
* **communication:** add tests for ActuatorSystem. ([ccfdbbb](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/ccfdbbbbd92c3563e5c9f01e1afb4957645902d5))
* **communication:** derive actuator input sizes. ([f6fccca](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f6fccca61b6c80db8d7a4eb5f59257ec12795cfa))
* **communication:** implement basic sensor. ([66ae8f3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/66ae8f3d5f5d373b19a75b45a41d7d5d5084f30e))
* **communication:** implement ROS2 subscriber and publisher functions ([6ffd5f1](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/6ffd5f1bb18bd69371efdabbc2ebd03508dd8e92))
* **communication:** implement sensor interface ([02665b0](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/02665b06e8132a9f320381a4fc62eee88866a1e4))
* **communication:** install ROS2 support for COMProtocol. ([38b2c4a](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/38b2c4aa67e4d40076f4d87092f51c890b4a08a9))
* **communication:** set/get actuator inputs through ActuatorSystem. ([0bc407d](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0bc407d514261cd90a087d0b704ee545016d95df))
* **communication:** use OnValidate() instead of Awake(). ([d30ab35](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/d30ab35c7c4bdda8fdb7ee7094ee379c2d4b59fa))
* **dynamics:** Change Step() to public. ([f4708ea](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f4708ea4377c5d71dd809ab3b5e92438629e8bd7))
* **dynamics:** implement DynamicSystem interface. ([301acb3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/301acb360d62a51c8060a79ba2cdc886401c30ba))
* **eigen3:** add exceptions to Matrix constructor. ([3b75001](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3b75001c64820ff71082d7323c9efa29423f92d8))
* **eigen3:** add operator coverage tests ([84f209b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/84f209b82d2a874cbd4df3135d80678d66240d14))
* **eigen3:** complete coverage tests for Matrix.Constructors. ([cb279ca](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/cb279ca77b30bd70c72036b3a89d23f851a94763))
* **motor:** add constraints to Integrator step size. ([441cc66](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/441cc66f979835d979b7a0060a38c576a786c992))
* **motor:** add ideal motor sensor ([9ee2ed8](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/9ee2ed8ef2b76617ad38d1e60ed8cd1f37c6b93c))
* **motor:** add motor position for IdealMotorSensor. ([8fb5211](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8fb521155a26bad1c7d585e9e5486b326fc3e67a))
* **motor:** add option to select integration method from editor. ([683c5b6](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/683c5b62d8e329fa10b82dc6b668d3639a521b66))
* **motor:** automate MotorLoad with Motor connections. ([b0eb91c](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/b0eb91cff979e4325e9ae6df39e881bd8be7207e))
* **motor:** MotorFunction is now abstract instead of a delegate. ([2daaf62](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/2daaf62c9a14dca903f44a147a9866eb696ad609))



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



# [0.1.0](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/413fab6935a115814d6cdd9509521718879a87a8...v0.1.0) (2023-10-16)


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



