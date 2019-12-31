import { writable } from 'svelte/store';

//should come from the server without testSelected or id, which should be mapped onto the results
export const availableTests = writable([
	// {	
	// 	testName:"Digital Input 1 Hi",
	// 	upperLimit:"PASS",
	// 	testValue:"",
	// 	lowerLimit:"PASS",
	// 	result:"",
  //   testStatus:"",
  //   id:0,
  //   testValueType:"PassFail"
  // },
	// {	
	// 	testName:"Digital Input 2 Hi",
	// 	upperLimit:"PASS",
	// 	testValue:"",
	// 	lowerLimit:"PASS",
	// 	result:"",
  //   testStatus:"",
  //   id:1,
  //   testValueType:"PassFail"
  // },
	// {	
	// 	testName:"Analog Input 1, 0-5V",
	// 	upperLimit:"4.5",
	// 	testValue:"",
	// 	lowerLimit:"4.7",
	// 	result:"",
  //   testStatus:"",
  //   id:2,
  //   testValueType:"Double"
  // }
    ]);


