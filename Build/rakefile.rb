require 'rake/clean'

DOT_NET_PATH = "#{ENV["SystemRoot"]}\\Microsoft.NET\\Framework\\v3.5"
NUNIT_EXE = "../ThirdParty/Nunit-2.5.0.9122/bin/net-2.0/nunit-console-x86.exe"
SOURCE_PATH = "../Source"
OUTPUT_PATH = "../Output"
CONFIG = "Debug"

CLEAN.include(OUTPUT_PATH)

task :default => ["clean", "all"]
task :all => [:compile, :test, :specs]
  
desc "Build solutions using MSBuild"
task :compile do
    solutions = FileList["#{SOURCE_PATH}/**/*.sln"].exclude(/\.2010\./)
    solutions.each do |solution|
    sh "#{DOT_NET_PATH}/msbuild.exe /p:Configuration=#{CONFIG} #{solution}"
end
end

desc "Runs tests with NUnit"
task :test => [:compile] do
    tests = FileList["#{OUTPUT_PATH}/**/NSubstitute.Specs.dll"].exclude(/obj\//)
    sh "#{NUNIT_EXE} #{tests} /nologo /exclude=Pending /xml=#{OUTPUT_PATH}/UnitTestResults.xml"
end

desc "Run acceptance specs with NUnit"
task :specs => [:compile] do
    tests = FileList["#{OUTPUT_PATH}/**/NSubstitute.Acceptance.Specs.dll"].exclude(/obj\//)
    sh "#{NUNIT_EXE} #{tests} /nologo /exclude=Pending /xml=#{OUTPUT_PATH}/SpecResults.xml"
end

desc "Runs pending acceptance specs with NUnit"
task :pending => [:compile] do
    acceptance_tests = FileList["#{OUTPUT_PATH}/**/NSubstitute.Acceptance.Specs.dll"].exclude(/obj\//)
    sh "#{NUNIT_EXE} #{acceptance_tests} /nologo /include=Pending /xml=#{OUTPUT_PATH}/PendingSpecResults.xml"
end

