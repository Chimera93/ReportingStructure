# ReportingStructure


# Approach
I took a true TDD style approach to this exercise, something I've been working on more during day to day development. I wrote tests that could not possibly go green for whatever reason to define my known (and assumed) use cases. Those tests were then able to prompt the logic I needed to implement.

# Issues
There are so many flavors of EFCore implementation, I ran into an issue where the EmployeeContext did not automatically serve up the DirectReports property, so I used an include to add that in.

# Post-mortem thoughts
- I didn't add them in the interest of time, but I would have been interested to add more test cases if more requirements were given. (Should adding a compensation auto create the Employee, etc)
- This was a nicely laid out exercise and I feel was able to effectively capture the intent of the test-ee rather than the ability to recite textbook knowledge or write down an unrealistic or random algorithm.
