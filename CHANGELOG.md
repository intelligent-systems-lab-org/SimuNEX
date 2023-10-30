# [0.4.0](https://github.com/intelligent-systems-lab-org/SimuNEX/compare/v0.3.1...v0.4.0) (2023-10-30)


### Bug Fixes

* **auv:** align stern plane origin. ([a2a230d](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a2a230dd6fef293e30150e1416a7797f8f94e725))
* **auv:** correct displaced volume factor. ([01d336a](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/01d336af800ab565e3ac6353dc1284667ccf97fe))
* **auv:** force perpendicular to flow and fin axis ([79761d9](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/79761d982fa64f4b0082ec0a24f2530f0e81a248))
* **auv:** kinetic energy calculation. ([a1e7ebb](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a1e7ebb744ad0965a99bc049bab75d2d32c182ad))
* **auv:** update auv prefab. ([5b988aa](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/5b988aaf53518ebd7a2648ba33b7d2046711732f))
* **dynamics:** add bidirectional thrust propeller. ([a6c68ee](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a6c68ee005d562f1f2d5d5cf4a8c3c382b67b360))
* **dynamics:** apply torque to _forces.angular. ([7ecf12c](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/7ecf12c9dd504db97ebea9ee5b03151063904027))
* **dynamics:** enable check for potential energy ([eff772c](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/eff772c7e17d1f1b8fcde0a3dbde9969833caab3))
* **dynamics:** remove coordinate frame argument. ([395919d](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/395919de3ba71549b2a62ade545bb53e0e1fe67c))
* **eigen3:** add determinant test. ([fb580dc](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/fb580dc57723d8645bca0fd3b47f0ca9f9f24ce3))
* **eigen3:** make Matrix serializable. ([1719323](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/17193236fcd854160bb72af11a41b2782f960956))
* **quadcopter:** change to box collider. ([329059e](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/329059e8c27fd0d6c98534be227f5a1466615c70))
* **quadcopter:** compress mesh ([22fcd06](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/22fcd0661283b5cb003f17aadca8b8b8602c7df1))
* **quadcopter:** update prefab with compressed mesh. ([0d95a5b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0d95a5b8d9180d1a0b0b4b5315aeae644f3b8cc1))
* **quadcopter:** use local space propeller location. ([9a8dfd9](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/9a8dfd98d02e075ee23d3233ef90675e545e64bd))


### Features

* **auv-simulation:** add AUV mesh ([58bf955](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/58bf9552642f501f1d928bfabe6407e346ca14e8))
* **auv-simulation:** add tests for Vector6DOF ([28a9b1f](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/28a9b1f613322c86b10b0a2b6756eb9a9bfecd49))
* **auv:** change drag coefficients to matrices. ([5028e87](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/5028e87cd915ef78f035b092afdf4e2ed6fb21cc))
* **auv:** implement added mass ([3f073b9](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/3f073b9e99b4b9eae253b212bd1a3c622263bf0c))
* **auv:** implement fins. ([585e73d](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/585e73d9471048bedaa6145e7db7bdc45a632d17))
* **auv:** set propulsion direction for fins. ([5c2f748](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/5c2f7484e029fec7e0a2dd5f0eb886b716ca87f2))
* **dynamics:** add applied force property. ([f44c7ac](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f44c7acbee60383309b6b9098bac948ec85899c5))
* **dynamics:** add buoyant force. ([2f9fcde](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/2f9fcde99ac1321e612c3b98e728feb86cb8f161))
* **dynamics:** add fluid-based rigidbody. ([8c723ac](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8c723ac1a62c76d9a8c2ad6641f7ddb04b2993df))
* **dynamics:** add independent gravity Force component. ([0c414d3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/0c414d3a54d1916c7e85e04d36621b422e2ba196))
* **dynamics:** add kinetic energy property. ([8623ac7](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8623ac7f96ede7a857bc079091119caf88998b00))
* **dynamics:** add linear drag. ([ea6211e](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/ea6211e8e5c265bd068d3cdcc9d0a07ca992a086))
* **dynamics:** add mechanical power property. ([05727b2](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/05727b2e7adf65b2c027477edf6a9c2dee9ce459))
* **dynamics:** add potential energy property. ([1ded9b4](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/1ded9b4685949ee028f4ac37a3ad652f0e7426ae))
* **dynamics:** add quadratic drag. ([b2d4ab3](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/b2d4ab3e3574268a6d843b53610f7629ae3437d4))
* **dynamics:** Add Vector6DOF ([b439670](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/b439670bf37cc0492a7c36393fcb45a08f592874))
* **dynamics:** add velocity property. ([cc04ca5](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/cc04ca56c579b3f65ccf5fe0718701bb719332a0))
* **dynamics:** automate COG and COB. ([bf89016](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/bf8901658acec1438393592729ecae87a115acad))
* **dynamics:** automate COG and COG. ([766c719](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/766c719e271ba36c7a2a773f71b5d99aa4b8f9cb))
* **dynamics:** velocity is now measured in the BCF. ([f040ae0](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f040ae0fcc8e7339e7aa6256f91f240576fbb350))
* **editor:** add Vector6DOFDrawer ([1d42dba](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/1d42dba8a6474f0ffc13dde3e8b5bf39c023c107))
* **eigen3:** add determinant property. ([8510da5](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8510da5d507508e025aeb04c96cc34ff5b261c0f))
* **environment:** add bounds detection. ([ed5d98b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/ed5d98b1a7834cf777f4bcf8c7fea5b291edccae))
* **environment:** add buoyancyfield. ([66cdf50](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/66cdf507e8a7aee45c231d3825ef89988c0fa39c))
* **environment:** add gravity field. ([8cfb371](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/8cfb371a2c93fac63c49c0ef579aee8491d518ad))
* **motor:** add saturation. ([6ff827f](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/6ff827fbdba3c50f7099d2508fbe29ab5ce713f4))
* **motor:** add torque measurement. ([5ff30cb](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/5ff30cbf0769952da0a250c84d3bd1449cbf80cb))
* **motor:** add total inertia calculation. ([16edf34](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/16edf34053b6207e619508271a7646b13160e1e9))
* **SimuNEX:** add foldout to Vector6DOF. ([4e2258b](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/4e2258b2f68bb4b10b160fada68345148211023f))
* **SimuNEX:** add Matrix-Vector6DOF conversion ([c4962cb](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/c4962cbd30f1efa9094c3fe7cf767378545b5281))
* **SimuNEX:** add Matrix6DOF drawer. ([83ffc40](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/83ffc4004e3e13d18730f453d759b7ddc20f6d19))
* **SimuNEX:** add Matrix6DOF operations. ([a81c257](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/a81c257bb720d68fae84e37e274e0c379784f1b2))
* **SimuNEX:** add Matrix6DOF type. ([dcb615e](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/dcb615e7fed4b966a0869e9f8fbb31ef5ffe700d))
* **SimuNEX:** test Matrix6DOF constructors. ([f72a310](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/f72a3101ddc81b1890c29f2a694cf62b9b7f45c8))
* **SimuNEX:** test Matrix6DOF Conversions. ([21343a1](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/21343a17b62670902e159be03318f29712ed45e1))
* **SimuNEX:** test Matrix6DOF Operations. ([ff0c639](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/ff0c639dc2d243d8811b7be000d7f2252c8d2117))
* **SimuNEX:** test Matrix6DOF properties. ([e1b9042](https://github.com/intelligent-systems-lab-org/SimuNEX/commit/e1b904286c18865dfe721d4438409f48a4a076ca))



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



